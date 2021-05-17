export class Group {
  public id: number;
  public number: string;
  public course: number;
  public specialityId: number;
  public headStudentId: number;

  public students: Student[];
  public student: Student;
  public speciality: Speciality;
}

export class Student {
  public id: number;
  public userEmail: string;
  public userFirstName: string;
  public userLastName: string;
  public userMiddleName: string;
  public userId: string;
  public group: Group;
  public subgroup: number;
}

export class Speciality {
  public id: number;
  public facultyid: number;
  public headStudentId: number;
  public code: string;
  public name: string;
  public abbreviature: string;

  public groups: Group[];
  public headStudent: Student;
  public faculty: Faculty;
}

export class Faculty {
  public id: number;
  public name: string;
  public abbreviation: string;
}

export class LessonType {
  id: number;
  name: string;
}

export class Lesson {
  public id: number;
  public dayScheduleId: number; 
  public lessonTime: string; 
  public subjectName: string; 
  public subGroup: string; 
  public lessonTypeId: number; 
  public auditory: string; 
  public weekNumber: string;
  public lessonType: LessonType; 
}

export class DaySchedule {
  public id: number; 
  public dayNumber: number; 
  public scheduleId: number; 
  public lessons: Lesson[]; 
}

export class Schedule {
  public id: number;
  public daySchedules: DaySchedule[];
}
