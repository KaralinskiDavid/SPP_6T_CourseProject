import { MatTableDataSource } from '@angular/material/table'
import { MatPaginator } from '@angular/material/paginator';
import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { StudentService } from '../services/student.service';
import { Student } from '../classes/iismodels';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddUserModal } from '../add-user-modal/add-user-modal.component';
import { ConfirmDeleteModalComponent } from '../confirm-delete-modal/confirm-delete-modal.component';
import { UserService } from '../services/user.service';
import { AuthService } from '../services/auth.service';
import { EditUserModalComponent } from '../edit-user-modal/edit-user-modal.component';
import { User } from '../classes/user';

@Component({
  selector: 'app-userstable',
  templateUrl: './userstable.component.html',
  styleUrls: ['./userstable.component.css']
})
export class UserstableComponent implements OnInit {

  students: Student[] = null;
  displayedColumns: string[] = ['email', 'firstName', 'surname', 'speciality', 'group', 'role', 'edit'];
  dataSource: MatTableDataSource<Student>;

  @ViewChild(MatPaginator, {static:false}) paginator: MatPaginator;

  constructor(private studentService: StudentService, private _userService: UserService, private _authService: AuthService, private toastr: ToastrService, private _modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadStudents();
  }

  loadStudents() {
    this.studentService.getStudents().subscribe((result: any) => {
      this.students = result;
      this.dataSource = new MatTableDataSource<Student>(this.students);
      this.dataSource.paginator = this.paginator;
    },
      error => {
        this.toastr.error("Something went wrong");
      }
    );
  }

  onDeleteClicked(element: Student) {
    const modalRef = this._modalService.open(ConfirmDeleteModalComponent);
    modalRef.componentInstance.user = element;
    modalRef.result.then((result) => {
      if (result == "Ok")
        this.deleteUser(element.userId);
    });
  }

  deleteUser(userId: string) {
    this._userService.deleteUser(userId).subscribe((response: boolean) => {
      if (response) {
        this.toastr.success("Deleted successful");
        this.students = this.students.filter(function (value: Student){
          return value.userId != userId;
        });
        this.dataSource = new MatTableDataSource<Student>(this.students);
      }
      else
        this.toastr.error("Something went wrong");
    },
      error => {
        this.toastr.error("Something went wrong");
      }
    );
  }

  onCreateClicked() {
    const modalRef = this._modalService.open(AddUserModal);
     modalRef.result.then((result) => {
      if (result.email!=null) {
        this._authService.signUp(result).subscribe((result: boolean) => {
          if (result) {
            this.toastr.success("Created");
            this.loadStudents();
          }
          else
            this.toastr.error("Something went wrong");
        },
          error => { this.toastr.error("Something went wrong"); }
        )
      };
     }).catch((res) => { });
  }

  onEditClicked(student: Student) {
    const modalRef = this._modalService.open(EditUserModalComponent);
    let user = new User();

    user.email = student.userEmail;
    user.firstName = student.userFirstName;
    user.lastName = student.userLastName;
    user.middleName = user.middleName;
    user.groupNumber = student.group.number;
    user.roleName = student.roleName;
    user.subGroup = student.subgroup;

    modalRef.componentInstance.editedUser = user;
    modalRef.result.then((result) => {
      if (result.email != null) {
        this._userService.updateUser(student.userId, user).subscribe((result: boolean) => {
          if (result) {
            this.toastr.success("Updated");
            this.loadStudents();
          }
          else
            this.toastr.error("Something went wrong");
        },
          error => { this.toastr.error("Something went wrong"); }
        )
      };
    }).catch((res) => { });
  }

}
