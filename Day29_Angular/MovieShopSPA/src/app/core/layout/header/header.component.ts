import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/models/user';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isAuth!: boolean;
  currentUser!: User;

  constructor(private authService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
    this.authService.isAuthenticated.subscribe(auth => {
      this.isAuth = auth;
      console.log('Auth Status:' + this.isAuth);
      if (this.isAuth) {
        this.authService.curentUser.subscribe((user: User) => {
          this.currentUser = user;
        });
        // this.userDataService.getallPurchasedMovies();
        // this.userDataService.purchasedMovies.subscribe(
        //   data => {
        //     if (data) {
        //       // console.log(data);
        //       this.myMoviesCount = data.purchasedMovies.length;
        //     }
        //   }
        // );
      }
    });
  }

  logout() {

    this.authService.logout();
    this.router.navigateByUrl('/login');
  }

}