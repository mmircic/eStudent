import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SubjectService } from 'src/app/services/subject.service';

@Component({
  selector: 'app-subject-editor',
  templateUrl: './subject-editor.component.html',
  styleUrls: ['./subject-editor.component.scss']
})
export class SubjectEditorComponent implements OnInit {

  form: FormGroup;

  constructor(private dialogRef: MatDialogRef<SubjectEditorComponent>, private formBuilder: FormBuilder, private subjectService: SubjectService) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      name: [null, Validators.required],
      ectsPoints: [null, Validators.required]
    });
  }

  onSave(){
    if (this.form.invalid) { 
      return;
    }

    let subject = this.form.value;

    this.subjectService.createSubject(subject).subscribe(
      res => {this.close(true);} ,
      error => {console.log(error);}
    );
  }

  close(data){
    this.dialogRef.close(data);
  }

}
