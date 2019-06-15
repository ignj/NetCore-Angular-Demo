import { AuthService } from './../services/auth.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  private authService;
  isExpanded = false;

  constructor(authService: AuthService) {
    this.authService = authService;
   }

  ngOnInit() {    
    this.authService.handleAuthentication();
    if (this.authService.isAuthenticated()) {
      this.authService.renewTokens();
    }    
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
