import { MatTableDataSource } from '@angular/material/table'
import { MatPaginator } from '@angular/material/paginator';
import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';

@Component({
  selector: 'app-specialitiestable',
  templateUrl: './specialitiestable.component.html',
  styleUrls: ['./specialitiestable.component.css']
})
export class SpecialitiestableComponent implements OnInit {

  displayedColumns: string[] = ['name', 'faculty', 'headman', 'groupsCount'];
  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);

  @ViewChild(MatPaginator, {static:false}) paginator: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  constructor() { }

  ngOnInit(): void {
  }

}

export interface PeriodicElement {
  name: string;
  faculty: string;
  headman: string;
  groupsCount: number;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
  { name: "Poit", faculty: "Ksis", headman: 'Hydrogen', groupsCount:10},
];
