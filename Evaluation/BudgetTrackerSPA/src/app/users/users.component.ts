import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../core/services/user.service';
import { UserDetails } from '../shared/models/UserRecordDetailsModel';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  id!: number;
  userDetails!: UserDetails[];

  constructor(private route:ActivatedRoute, private userService: UserService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      params => {
        this.id = Number(params.get('id'));
        console.log('User Id:' + this.id);

        // call the MovieService that will call the Movie Details API.
        this.userService.GetUserDetailsById(this.id).subscribe(
          m => {
            this.userDetails = m;
            console.table(this.userDetails);
          }
        );

      }
    )
  }

}
