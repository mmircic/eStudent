import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CourseService } from 'src/app/services/course.service';
import { Course } from 'src/app/models/course.model';
import { CourseType } from 'src/app/models/course-type.model';
import { CourseTypeService } from 'src/app/services/course-type.service';

@Component({
  selector: 'app-course-editor',
  templateUrl: './course-editor.component.html',
  styleUrls: ['./course-editor.component.scss']
})
export class CourseEditorComponent implements OnInit {

  form: FormGroup;
  courseCreate = true;
  course: Course;
  courseTypes: Array<CourseType>;
  errors: any;

  constructor(private dialogRef: MatDialogRef<CourseEditorComponent>, private formBuilder: FormBuilder, private courseService: CourseService, private courseTypeService: CourseTypeService) { 
    this.course = courseService.Course;
  } 

  ngOnInit() {
    
    if (this.course !== undefined) {
      this.courseCreate = false;
    }
    this.courseTypeService.getAllCourseTypes().subscribe(ct => this.courseTypes = ct);

    this.form = this.formBuilder.group({
      name: [null, Validators.compose([Validators.required])],
      courseType: [null, Validators.required]
    });

    if (!this.courseCreate) {
      let extend_json = Object.assign({}, this.course);
      delete extend_json['id'];
      delete extend_json['courseTypeId'];
      delete extend_json['subjectCourses'];
      delete extend_json['userCourses'];
      this.form.setValue(extend_json);
      this.form.controls['courseType'].setValue(extend_json['courseType'].id);
    }   
  }

  close(data){
    this.dialogRef.close(data);
  }

  onSave(){
    console.log(this.courseCreate
      );
    if (this.form.invalid) {
      return;
    }

    let extend_json = Object.assign(this.form.value);
    delete extend_json['courseType'];
    extend_json['courseTypeId'] = this.form.controls['courseType'].value;

    if (this.courseCreate) {
      console.log(extend_json);
      this.courseService.createCourse(extend_json).subscribe(
        res => {this.close(true);} ,
        error => {console.log(error);}
      );
      //ovo ce izvrsiti prije nego odes na api

    }
    else{
      let extend_json = this.form.value;
      this.courseService.updateCourse(this.course.id, extend_json).subscribe( 
        res => { this.close(true); },
        error => { console.log(error.error);}
      );
    }
  }

}
