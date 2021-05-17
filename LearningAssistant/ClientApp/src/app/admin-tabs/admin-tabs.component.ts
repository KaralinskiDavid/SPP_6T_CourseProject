import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-tabs',
  templateUrl: './admin-tabs.component.html',
  styleUrls: ['./admin-tabs.component.css']
})
export class AdminTabsComponent implements OnInit {

  navLinks: any[];
  activeLinkIndex = -1;
  constructor(private router: Router) {
    this.navLinks = [
      {
        label: 'Users',
        link: '../userstable',
        index: 0
      }, {
        label: 'Specialities',
        link: '../specialitiestable',
        index: 1
      }, {
        label: 'Groups',
        link: '../groupstable',
        index: 2
      },
    ];
  }

  ngOnInit(): void {
    this.router.events.subscribe((res) => {
      this.activeLinkIndex = this.navLinks.indexOf(this.navLinks.find(tab => tab.link === '.' + this.router.url));
    });
  }

}
