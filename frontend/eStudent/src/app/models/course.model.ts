import { CourseType } from "./course-type.model";

export interface Course {
    id: number;
    name: string;
    courseType: CourseType;
}
