import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';
import { Observable, throwError } from 'rxjs';
import { User } from '../models/user.model';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  API_BASE_URL = environment.baseUri;

  constructor(private http: HttpClient) { }

  getAllStudents(): Observable<Array<User>>{
    return this.http.get<Array<User>>(this.API_BASE_URL + "student/all");
  }

}
