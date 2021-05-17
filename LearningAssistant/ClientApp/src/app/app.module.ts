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
import { ScheduleTableComponent } from './schedule-table/schedule-table.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { ConfirmDeleteModalComponent } from './confirm-delete-modal/confirm-delete-modal.component';

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

const appRoutes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: "register",
    component: RegisterComponent,
  },
  {
  path: "userstable",
  component: UserstableComponent,
  },
  {
    path: "specialitiestable",
    component: SpecialitiestableComponent,
  },
  {
    path: "schedulestable",
    component: ScheduleTableComponent,
  },
  {
    path: "adminTabs",
    component: AdminTabsComponent,
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
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }],
  entryComponents: [AddUserModal, ConfirmDeleteModalComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
