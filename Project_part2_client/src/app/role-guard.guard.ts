import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth/auth.service';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(public authService: AuthService, public router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRoles: string[] = route.data['expectedRoles'];
    const token = this.authService.getToken();
    if (!token) {
      this.router.navigate(['login']);
      return false;
    }
    const tokenPayload = jwt_decode(token);
    if (!this.tokenHasExpectedRole(tokenPayload, expectedRoles)) {
      this.router.navigate(['login', {notAuthorized: true}]);
      return false;
    }
    return true;
  }

  private tokenHasExpectedRole(tokenPayload: any, expectedRoles: string[]): boolean {
    if(!tokenPayload) return false;
    const userRoles: string[] = tokenPayload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    if (typeof userRoles === 'string') {
      return expectedRoles.includes(userRoles);
    }
    return expectedRoles.some(r => userRoles.indexOf(r) >= 0);
  }
}
