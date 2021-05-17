import { MatTableDataSource } from '@angular/material/table'
import { MatPaginator } from '@angular/material/paginator';
import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { GroupService } from '../services/group.service';
import { Group } from '../classes/iismodels';

@Component({
  selector: 'app-groupstable',
  templateUrl: './groupstable.component.html',
  styleUrls: ['./groupstable.component.css']
})
export class GroupstableComponent implements OnInit {

  displayedColumns: string[] = ['number', 'faculty', 'speciality', 'headman', 'studentsCount', 'course'];
  groups: Group[];
  dataSource: MatTableDataSource<Group>;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private groupService: GroupService) { }

  ngOnInit(): void {
    this.groupService.getGroups().subscribe((result: any) => {
      this.groups = result;
      this.dataSource = new MatTableDataSource<Group>(this.groups);
      this.dataSource.paginator = this.paginator;
    });
  }

}


