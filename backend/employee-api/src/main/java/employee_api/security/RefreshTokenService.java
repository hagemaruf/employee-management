package employee_api.security;

import employee_api.entity.RefreshToken;
import employee_api.entity.User;
import employee_api.repository.RefreshTokenRepository;
import employee_api.exception.InvalidRefreshTokenException;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.security.SecureRandom;
import java.time.Instant;
import java.util.Base64;

@Service
public class RefreshTokenService {

    private final RefreshTokenRepository refreshTokenRepository;

    @Value("${jwt.refresh-expiration:604800000}")
    private long refreshExpiration;

    public RefreshTokenService(
            RefreshTokenRepository refreshTokenRepository) {
        this.refreshTokenRepository = refreshTokenRepository;
    }

    public RefreshToken createRefreshToken(User user) {

        String token = generateToken();

        RefreshToken refreshToken = new RefreshToken();

        refreshToken.setUser(user);
        refreshToken.setToken(token);
        refreshToken.setCreatedAt(Instant.now());
        refreshToken.setExpiresAt(
                Instant.now().plusMillis(refreshExpiration)
        );

        return refreshTokenRepository.save(refreshToken);
    }

    public RefreshToken findByToken(String token) {

        return refreshTokenRepository
                .findByToken(token)
                .orElseThrow(() ->
                        new RuntimeException("Refresh token not found"));
    }

    public RefreshToken verifyExpiration(RefreshToken token) {

        if (token.getRevokedAt() != null) {

            throw new InvalidRefreshTokenException(
                    "Refresh token has been revoked"
            );
        }

        if (token.getExpiresAt().isBefore(Instant.now())) {

            refreshTokenRepository.delete(token);

            throw new InvalidRefreshTokenException(
                    "Refresh token has expired"
            );
        }

        return token;
    }

    public void revokeToken(RefreshToken token) {

        token.setRevokedAt(Instant.now());

        refreshTokenRepository.save(token);
    }

    public RefreshToken rotateRefreshToken(String token) {

        RefreshToken oldToken = findByToken(token);

        verifyExpiration(oldToken);

        // Revoke old refresh token
        revokeToken(oldToken);

        // Generate new refresh token
        return createRefreshToken(oldToken.getUser());
    }

    private String generateToken() {

        byte[] randomBytes = new byte[64];

        new SecureRandom().nextBytes(randomBytes);

        return Base64.getUrlEncoder()
                .withoutPadding()
                .encodeToString(randomBytes);
    }
}