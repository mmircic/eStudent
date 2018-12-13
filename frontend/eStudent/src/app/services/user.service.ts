import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';
import { catchError, map } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  

  API_BASE_URL = environment.baseUri;
  User: User;

  constructor(private http: HttpClient) { }

  getAllStudents(): Observable<Array<User>> {
    return this.http.get<Array<User>>(this.API_BASE_URL + "student/all");
  }

  deleteUser(id: number): Observable<{}> {
    return this.http.delete(this.API_BASE_URL + 'user/' + id);
  }

  createStudent(user: any) {

    return this.http.post(this.API_BASE_URL + 'user', user, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  updateStudent(id: number, user) {
    return this.http.put(this.API_BASE_URL + 'user/' + id, user, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }
}



