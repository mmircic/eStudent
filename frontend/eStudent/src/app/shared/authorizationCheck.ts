import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService, TOKEN_NAME } from '../services/authentication.service';

@Injectable()
export class AuthorizationCheck implements CanActivate {

  constructor(private router: Router, private auth: AuthenticationService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    //If token data exist, user may login to application
    if (this.auth.isLoggedIn()) {
      return true;
    }

    // otherwise redirect to login page with the return url
    localStorage.removeItem(TOKEN_NAME);
    
    this.router.navigate([''], { queryParams: { returnUrl: state.url } });
    return false;
  }
}