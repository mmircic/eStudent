import { User } from "./user.model";
import { Course } from "./course.model";

export interface UserCourse {

    id: number;
    user: User;
    course: Course;
    date: string;
    accepted: boolean;
}
