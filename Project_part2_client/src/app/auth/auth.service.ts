import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from './login-request';
import { LoginResult } from './login-result';
import { environment } from '../environment/environment';
import { Observable, Subject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  tokenKey: string = 'jwt-token';

  private _authStatus = new Subject<boolean>();
  public authStatus = this._authStatus.asObservable();
  private _role: string | null = null;

  init(): void {
    if (this.isAuthenticated()) {
      this.setAuthStatus(true);
    }
  }

  setAuthStatus(isAuthenticated: boolean) {
    this._authStatus.next(isAuthenticated);
  }

  constructor(protected http: HttpClient) { }

  isAuthenticated() : boolean {
    return this.getToken() != null;
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  login(item:LoginRequest):Observable<LoginResult>{
    var url = environment.baseUrl + 'api/Account';

    return this.http.post<LoginResult>(url, item)
      .pipe(tap( (loginResult:LoginResult) => {
        if(loginResult.success && loginResult.token) {
          localStorage.setItem(this.tokenKey, loginResult.token);
          this._role = loginResult.role;
          this.setAuthStatus(true);
        }
      }))
  }

  getRole(): string | null {
    return this._role;
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this._role = null;
    this.setAuthStatus(false);
  }
}
