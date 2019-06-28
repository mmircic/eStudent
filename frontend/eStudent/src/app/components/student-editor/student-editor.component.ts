import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user.model';
import { ValidationService } from 'src/app/services/validation.service';


@Component({
  selector: 'app-student-editor',
  templateUrl: './student-editor.component.html',
  styleUrls: ['./student-editor.component.scss']
})
export class StudentEditorComponent implements OnInit {

  form: FormGroup;
  user: User;
  private NameRegex = /^([A-Za-zČčĆćĐđŽžŠš]+[,.]?[ ]?|[A-Za-zČčĆćĐđŽžŠš]+['-]?)+$/;
  private CityNameRegex = /^(?:[a-zA-Z]+(?:[.'\-,])?\s?)+$/;
  private createStudent = true;
  private errors: {code: string, description: string}[];

  constructor(private dialogRef: MatDialogRef<StudentEditorComponent>, private formBuilder: FormBuilder,
    private userService: UserService, private validationService: ValidationService) {
    this.user = userService.User;
    if(this.user !== undefined) {
      this.createStudent = false;
    } 
  }

  ngOnInit() {

    this.form = this.formBuilder.group({
      oib: [null, Validators.compose([Validators.required, Validators.minLength(11), Validators.maxLength(11), Validators.pattern(/^[0-9]*$/)])],
      firstName: [null, Validators.compose([Validators.required, Validators.pattern(this.NameRegex)])],
      lastName: [null, Validators.compose([Validators.required, Validators.pattern(this.NameRegex)])],
      birthDate: [null, Validators.required],
      residence: [null, Validators.compose([Validators.required, Validators.pattern(this.CityNameRegex)])],
      email: [null, Validators.compose([Validators.required, Validators.email])],
      password: [null, Validators.compose([Validators.required, Validators.minLength(5)])],
      repeatedPassword: [null, Validators.compose([Validators.required, Validators.minLength(5), this.validationService.passwordConfirmValidaton])]
    });

    if (!this.createStudent) {
      let extend_json = Object.assign({},this.user);
      delete extend_json['id'];
      delete extend_json['role'];
      delete extend_json['password'];
      this.form.removeControl('password');
      this.form.removeControl('repeatedPassword');
      this.form.setValue(extend_json);
    }
  }

  close(data) {
    this.dialogRef.close(data);
  }

  createErrors = {
    "DefaultError": "Generalna Greška",
    "ConcurrencyFailure": "",
    "PasswordMismatch": "Neodgovarajuća lozinka",
    "InvalidToken": "Pogrešan token",
    "LoginAlreadyAssociated": "Prijava je već izvršena",
    "InvalidUserName": "Pogrešno kiorisničko ime",
    "InvalidEmail": "Pogrešna Email adresa",
    "DuplicateUserName": null,
    "DuplicateEmail": "Email adresa već postoji",
    "InvalidRoleName": "Pogrešan naziv uloge",
    "DuplicateRoleName": "Uloga već postoji",
    "UserAlreadyHasPassword": "Lozinka već postoji",
    "UserLockoutNotEnabled": "",
    "UserAlreadyInRole": "",
    "UserNotInRole": "",
    "PasswordTooShort": "Lozinka je prekratka",
    "PasswordRequiresNonAlphanumeric": "Lozinka mora sadržavati bar 1 znak",
    "PasswordRequiresDigit": "Lozinka mora sadržavati bar 1 broj",
    "PasswordRequiresLower": "Lozinka mora sadržavati bar jedno malo slovo",
    "PasswordRequiresUpper": "Lozinka mora sadržavati bar jedno veliko slovo",
    "DuplicateOIB": "OIB već postoji"
  };

  onSave() {
    if (this.form.invalid) {
      return;
    }

    if (this.createStudent) {
      
      let password = this.form.controls.password.value;
      let extend_json = this.form.value;
      delete extend_json['repeatedPassword'];
      delete extend_json['password'];
      extend_json['passwordHash'] = password;

      this.userService.createStudent(extend_json).subscribe(
        res => {
          this.close(true);
        }, error => {
          this.errors = error.error;
          for (let err of this.errors) {
            console.log(err);
            err.description = this.createErrors[err.code];
          }
          console.log(this.errors);
        });
      //ovo ce izvrsiti prije nego odes na api

    }
    else{
      let extend_json = this.form.value;
      this.userService.updateStudent(this.user.id, extend_json).subscribe( res => {
        this.close(true);
      }, error => { 
        this.errors = error.error;
        for (let err of this.errors) {
          console.log(error.error);
          err.description = this.createErrors[err.code];
        }
        console.log(this.errors);
      });
    }
  }



}
