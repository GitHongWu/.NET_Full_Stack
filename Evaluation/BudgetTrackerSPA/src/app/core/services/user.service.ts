import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserProfile } from 'src/app/shared/models/UserProfileResponseModel';
import { UserDetails } from 'src/app/shared/models/UserRecordDetailsModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  GetAllUsers(): Observable<UserProfile[]> {
    return this.http.get(`${environment.apiUrl}${'Users'}`)
      .pipe(map(resp => resp as UserProfile[]))
  }

  GetUserDetailsById(Id: number): Observable<UserDetails[]> {
    return this.http.get(`${environment.apiUrl}${'Users/Details/'}${Id}`)
      .pipe(map(resp => resp as UserDetails[]))
  }
}
