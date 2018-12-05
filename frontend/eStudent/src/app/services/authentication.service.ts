import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from "rxjs/operators";
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import * as jwt_decode from "jwt-decode";
import { User } from '../models/user.model';

export const TOKEN_NAME = environment.tokenName;

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  API_BASE_URL = environment.baseUri;
  user: User;

  constructor(private http: HttpClient, private router: Router) {
    if (this.isLoggedIn()) {
      this.user = jwt_decode(localStorage.getItem(TOKEN_NAME));
    } 
    
  } 

  login(email: string, password: string){
    return this.http.post<any>(this.API_BASE_URL + 'auth',{email, password}, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(map(user => {
      if (user && user.token) {
        localStorage.setItem(TOKEN_NAME, JSON.stringify(user));
      }
      
      return user;
    }));
  }

  logout(){
    localStorage.removeItem(TOKEN_NAME);
    this.router.navigate(['']);
  }

  isLoggedIn(){
    if (localStorage.getItem(TOKEN_NAME) && !this.isTokenExpired()) {
      
      return true;
    }

    return false;
  }

  getTokenExpirationDate(token: string): Date {
    const decoded = jwt_decode(token);

    if (decoded.exp === undefined) {
      return null;
    }

    const date = new Date(0);
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  isTokenExpired(token?: string): boolean{
    if(!token) token = localStorage.getItem(TOKEN_NAME);
    if(!token) return true;

    const date = this.getTokenExpirationDate(token);
    if(date === undefined) return false;
    return (date.valueOf() < new Date().valueOf());
  }
}
