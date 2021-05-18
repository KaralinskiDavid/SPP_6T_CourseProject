import { MatTableDataSource } from '@angular/material/table'
import { MatPaginator } from '@angular/material/paginator';
import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { SpecialitiesService } from '../services/speciality.service';
import { Speciality } from '../classes/iismodels';

@Component({
  selector: 'app-specialitiestable',
  templateUrl: './specialitiestable.component.html',
  styleUrls: ['./specialitiestable.component.css']
})
export class SpecialitiestableComponent implements OnInit {

  displayedColumns: string[] = ['name', 'faculty', 'headman', 'groupsCount'];
  specialities: Speciality[];
  dataSource: MatTableDataSource<Speciality>;

  @ViewChild(MatPaginator, {static:false}) paginator: MatPaginator;

  constructor(private _specialityService: SpecialitiesService) { }

  ngOnInit(): void {
    this._specialityService.getSpecialitites().subscribe((result: Speciality[]) => {
      this.specialities = result;
      this.dataSource = new MatTableDataSource<Speciality>(this.specialities);
      this.dataSource.paginator = this.paginator;
    },
      error => {}
    )
  }

}

export interface PeriodicElement {
  name: string;
  faculty: string;
  headman: string;
  groupsCount: number;
}
