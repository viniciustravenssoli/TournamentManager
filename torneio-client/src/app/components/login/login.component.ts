import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthServiceService } from 'src/app/services/auth-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user = { email: '', password: '' };

  constructor(private authService: AuthServiceService, private router: Router) {}

  login(): void {
    this.authService.login(this.user).subscribe(
      (response) => {
        this.authService.saveToken(response.token);
        console.log(response);
        this.router.navigate(['/']);
      },
      (error) => {
        console.error('Login failed', error);
      }
    );
  }
}
