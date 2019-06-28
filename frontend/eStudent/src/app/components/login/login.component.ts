import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService, TOKEN_NAME } from 'src/app/services/authentication.service';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  submitClick = false;
  submitted = false;
  returnUrl: string;
  invalidLogin = false;
  hide = true;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private auth: AuthenticationService) { }

  ngOnInit() {
    if (this.auth.isLoggedIn()) {
      this.router.navigate(['/student-list']);
    }else{
      localStorage.removeItem(TOKEN_NAME);
    }

    this.loginForm = this.formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.required]
    });    

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/student-list';
  }

  get formData() { return this.loginForm.controls}

  onLogin(){
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.submitClick = true;
    this.auth.login(this.formData.email.value, this.formData.password.value)
    .pipe(first())
    .subscribe(data => {
      this.router.navigate([this.returnUrl]);
    },
    error => {
      this.invalidLogin = true;
      this.submitClick = false;
    });
  }

  

  

}
