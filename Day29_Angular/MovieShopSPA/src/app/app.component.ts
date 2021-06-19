import { Component } from '@angular/core';
import { AuthenticationService } from './core/services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Movie Shop Angular Application SPA';

  constructor(private authService: AuthenticationService) {
    authService.populateUserData();
  }
}
