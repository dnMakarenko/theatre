import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }
  readonly baseURI = 'http://localhost:54277/api';
  public currentUser;

  InitCurrentUser() {
    if (this.isAuthenticated()) {
      this.currentUser = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    }
  }

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', Validators.email],
    FullName: [''],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })

  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FullName: this.formModel.value.FullName,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(this.baseURI + '/Account/SignUp', body);
  }

  login(formData) {
    return this.http.post(this.baseURI + '/Account/Authenticate', formData);
  }

  getUserProfile() {
    return this.http.get(this.baseURI + '/UserProfile');
  }

  roleMatch(allowedRoles): boolean {
    for (let role of allowedRoles) {
      if (this.currentUser.role === role) {
        return true;
      }
    }

    return false;
  }

  isAuthenticated(): boolean {
    if (localStorage.getItem('token') !== null) {
      return true;
    }

    return false;
  }

  isUserInRole(role): boolean {
    if (!this.isAuthenticated()) {
      return false;
    }
    if (role === this.currentUser.role) {
      return true;
    }
  }

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/spectacles']);
  }
}
