import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(        
        private authService: AuthService,
        private router: Router) { }

    canActivate() {
        if(this.authService.isAuthenticated()){            
            return true;
        }else{            
            this.router.navigate(["/home"]);
            return false;
        }
    }
}