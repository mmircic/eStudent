import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { AuthenticationService, TOKEN_NAME } from "../services/authentication.service";
import { Injectable } from "@angular/core";

@Injectable()
export class AdministratorCheck implements CanActivate{
    constructor(private router: Router, private auth: AuthenticationService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

        if (this.auth.isAdministrator()) {
            return true;
        }

        //return false;

        if(this.auth.isLoggedIn()){
            this.router.navigate(['course-list']);
            //return false; 
        }
        
        //localStorage.removeItem(TOKEN_NAME);
    
        // this.router.navigate([''], { queryParams: { returnUrl: state.url } });
        // return false;
  }
}
