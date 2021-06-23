import { Component, OnInit } from '@angular/core';
import { UserService } from '../core/services/user.service';
import { UserProfile } from '../shared/models/UserProfileResponseModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private userService: UserService) { }

  userProfiles!: UserProfile[];

  ngOnInit(): void {
    this.userService.GetAllUsers().subscribe(
      m => {
        this.userProfiles = m;
        console.log('inside Home Component');
        console.log(this.userProfiles);
      }
    );
  }
}
