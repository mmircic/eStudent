import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  rootUrl: string = "https://localhost:44391/"

  constructor(private http: HttpClient) { }

  login(email: string, password: string){
    return this.http.post("api/login",{email, password}).pipe(map(user => {
      if (user && user.token) {
        localStorage.setItem("TokenInfo", JSON.stringify(user));
      }

      return user;
    }));
  }

  logout(){
    localStorage.removeItem("TokenInfo");
  }
}
