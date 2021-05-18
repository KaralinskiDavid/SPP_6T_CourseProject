import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTabsModule } from '@angular/material/tabs';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AddUserModal } from './add-user-modal/add-user-modal.component';
import { EditUserModalComponent } from './edit-user-modal/edit-user-modal.component';
import { ScheduleTableComponent } from './schedule-table/schedule-table.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { ConfirmDeleteModalComponent } from './confirm-delete-modal/confirm-delete-modal.component';
import { QueueEditorComponent } from './queue-editor/queue-editor.component';
import { MatButtonModule } from '@angular/material/button';
import { AddQueueModalComponent } from './add-queue-modal/add-queue-modal.component';
import { MatListModule } from '@angular/material/list';
import { UserTabsComponent } from './user-tabs/user-tabs.component';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserstableComponent } from './userstable/userstable.component';
import { SpecialitiestableComponent } from './specialitiestable/specialitiestable.component';
import { GroupstableComponent } from './groupstable/groupstable.component';
import { AdminTabsComponent } from './admin-tabs/admin-tabs.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CDK_DRAG_CONFIG, DragDropModule } from '@angular/cdk/drag-drop';

const DragConfig = {
  dragStartThreshold: 0,
  pointerDirectionChangeThreshold: 5,
  zIndex: 10000
};

const appRoutes: Routes = [
  {
    path: '',
    component: LoginComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: "register",
    component: RegisterComponent,
  },
  {
    path: "admin",
    component: AdminTabsComponent,
  },
  {
    path: "user",
    component: UserTabsComponent,
  },
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegisterComponent,
    UserstableComponent,
    SpecialitiestableComponent,
    GroupstableComponent,
    AdminTabsComponent,
    AddUserModal,
    ScheduleTableComponent,
    ConfirmDeleteModalComponent,
    QueueEditorComponent,
    EditUserModalComponent,
    AddQueueModalComponent,
    UserTabsComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MDBBootstrapModule.forRoot(),
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true }
    ),
    ToastrModule.forRoot({ "closeButton": false, "enableHtml": true, "timeOut": 5000, positionClass: 'toast-bottom-right', }),
    NgbModule,
    MatTableModule,
    MatPaginatorModule,
    MatTabsModule,
    MatInputModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSlideToggleModule,
    MatSelectModule,
    DragDropModule,
    MatButtonModule,
    MatListModule,
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }, { provide: CDK_DRAG_CONFIG, useValue: DragConfig }],
  entryComponents: [AddUserModal, ConfirmDeleteModalComponent, EditUserModalComponent, AddQueueModalComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
