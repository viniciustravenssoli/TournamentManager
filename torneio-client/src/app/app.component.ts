import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthServiceService } from './services/auth-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'torneio-client';

  constructor(private authService: AuthServiceService, private router: Router) { }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  logout(): void {
    if (confirm('Are you sure you want to log out?')) {
      this.authService.logout();
      this.router.navigate(['/register']);
    }
  }
}
