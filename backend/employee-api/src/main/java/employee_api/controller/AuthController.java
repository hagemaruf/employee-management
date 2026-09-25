package employee_api.controller;

import employee_api.dto.AuthResponse;
import employee_api.dto.LoginRequest;
import employee_api.dto.RefreshTokenRequest;
import employee_api.entity.RefreshToken;
import employee_api.entity.User;
import employee_api.repository.UserRepository;
import employee_api.security.JwtService;
import employee_api.security.RefreshTokenService;

import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/auth")
public class AuthController {

    private final AuthenticationManager authenticationManager;
    private final JwtService jwtService;
    private final UserRepository userRepository;
    private final RefreshTokenService refreshTokenService;

    public AuthController(
            AuthenticationManager authenticationManager,
            JwtService jwtService,
            UserRepository userRepository,
            RefreshTokenService refreshTokenService) {

        this.authenticationManager = authenticationManager;
        this.jwtService = jwtService;
        this.userRepository = userRepository;
        this.refreshTokenService = refreshTokenService;
    }

    @PostMapping("/login")
    public AuthResponse login(@RequestBody LoginRequest request) {

        Authentication authentication =
                authenticationManager.authenticate(
                        new UsernamePasswordAuthenticationToken(
                                request.getUsername(),
                                request.getPassword()
                        )
                );

        String username = authentication.getName();

        User user = userRepository
                .findByUsername(username)
                .orElseThrow(() ->
                        new RuntimeException("User not found"));

        String accessToken =
                jwtService.generateToken(username);

        RefreshToken refreshToken =
                refreshTokenService.createRefreshToken(user);

        return new AuthResponse(
                accessToken,
                refreshToken.getToken(),
                "Bearer",
                900
        );
    }

   @PostMapping("/refresh")
   public AuthResponse refresh(@RequestBody RefreshTokenRequest request) {

        RefreshToken newRefreshToken =
                refreshTokenService.rotateRefreshToken(
                        request.getRefreshToken()
                );

        String username =
                newRefreshToken.getUser().getUsername();

        String newAccessToken =
                jwtService.generateToken(username);

        return new AuthResponse(
                newAccessToken,
                newRefreshToken.getToken(),
                "Bearer",
                900
        );
    }
}