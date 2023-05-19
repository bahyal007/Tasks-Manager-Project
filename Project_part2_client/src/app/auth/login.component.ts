import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { LoginResult } from './login-result';
import { LoginRequest } from './login-request';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form!: UntypedFormGroup;
  loginResult!: LoginResult;
  notAuthorized: boolean = false;

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      userName: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required)
    });

    this.route.params.subscribe(params => {
      this.notAuthorized = params['notAuthorized'] || false;
    });
  }

  onSubmit() {
    var loginRequest = <LoginRequest>{
      userName: this.form.controls['userName'].value,
      password: this.form.controls['password'].value
    };
    this.authService.login(loginRequest).subscribe({
      next: result => {
        console.log(result);
        this.loginResult = result;
        if (result.success) {
          localStorage.setItem(this.authService.tokenKey, result.token);
          this.router.navigate(["/"]);
        }
      },
      error: error => {
        console.log(error);
        if (error.status == 401) {
          loginRequest = error.error;
        }
    }});
  }
}

