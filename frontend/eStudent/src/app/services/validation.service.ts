import { Injectable } from '@angular/core';
import { FormControl, AbstractControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {

  constructor() { }

  passwordConfirmValidaton(c: AbstractControl): any{
    if(!c.parent || !c) return;
    const password = c.parent.get('password'); 
    const repeatedPassword = c.parent.get('repeatedPassword'); 
    if (password.value !== repeatedPassword.value) {
      return { invalid: true };
    }
  }
}
