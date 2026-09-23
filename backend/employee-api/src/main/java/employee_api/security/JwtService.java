package employee_api.security;

import io.jsonwebtoken.JwtException;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.security.Keys;
import org.springframework.stereotype.Service;

import javax.crypto.SecretKey;
import java.util.Date;

@Service
public class JwtService {

    private final SecretKey secretKey =
            Keys.hmacShaKeyFor(
                    "my-super-secret-key-for-employee-api-2026"
                            .getBytes()
            );

    private final long expirationTime = 1000 * 60 * 60; // 1 hour

    public String generateToken(String username) {

        Date now = new Date();

        return Jwts.builder()
                .subject(username)
                .issuedAt(now)
                .expiration(
                        new Date(now.getTime() + expirationTime)
                )
                .signWith(secretKey)
                .compact();
    }

    public String extractUsername(String token) {

        return Jwts.parser()
                .verifyWith(secretKey)
                .build()
                .parseSignedClaims(token)
                .getPayload()
                .getSubject();
    }

    public boolean isTokenValid(
        String token,
        org.springframework.security.core.userdetails.UserDetails userDetails) {

        try {

                String username = extractUsername(token);

                return username.equals(userDetails.getUsername());

        } catch (JwtException | IllegalArgumentException e) {

                return false;
        }
    }
}