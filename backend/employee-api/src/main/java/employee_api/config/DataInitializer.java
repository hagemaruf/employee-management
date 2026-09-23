package employee_api.config;

import employee_api.entity.User;
import employee_api.repository.UserRepository;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.crypto.password.PasswordEncoder;

@Configuration
public class DataInitializer {

    @Bean
    CommandLineRunner initUsers(
            UserRepository userRepository,
            PasswordEncoder passwordEncoder) {

        return args -> {

            if (userRepository.findByUsername("maruf").isEmpty()) {

                User user = new User(
                        "maruf",
                        passwordEncoder.encode("password123"),
                        "ADMIN"
                );

                userRepository.save(user);

                System.out.println("Default user 'maruf' created.");
            }
        };
    }
}