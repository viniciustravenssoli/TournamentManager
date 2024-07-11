import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthServiceService } from 'src/app/services/auth-service.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  user = { userName: '', email: '', password: '' };

  constructor(private authService: AuthServiceService, private router: Router) {}

  register(): void {
    this.authService.register(this.user).subscribe(
      (response) => {
        this.authService.saveToken(response.token);
        this.router.navigate(['/']);
      },
      (error) => {
        console.error('Registration failed', error);
      }
    );
  }
}
