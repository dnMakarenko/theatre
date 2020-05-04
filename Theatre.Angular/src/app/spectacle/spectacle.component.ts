import { SpectacleService } from './../spectacle/spectacle.service';
import { UserService } from './../shared/user.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-spectacle',
  templateUrl: './spectacle.component.html',
  styles: []
})
export class SpectacleComponent implements OnInit {
  spectacles;
  response;
  currentSpectacle;
  currentPageNumber: number;
  pageSize: number;
  totalRecords: number;
  skip: number;

  @Output() changeTablePage = new EventEmitter<number>();
  @Output() onRefresh = new EventEmitter<boolean>();

  constructor(private router: Router, private spectacleService: SpectacleService, private userService: UserService) {
  }

  ngOnInit() {
    this.pageSize = 9;
    this.currentPageNumber = 1;
    this.skip = 0;

    this.spectacleService.GetFiltered(this.skip, this.pageSize).subscribe(
      res => {
        this.response = res;
        this.spectacles = this.response.body;
        this.totalRecords = this.response.totalRecords;
      },
      err => {
        console.log(err);
      },
    );

    this.changeTablePage.emit(this.skip);
  }

  Details(id: string)
  {
    this.spectacleService.GetById(id).subscribe(
      res => {
        this.currentSpectacle = res;
      },
      err =>
      {
        console.log(err);
      })
  };

  Delete(entity) {
    this.spectacleService.Delete(entity).subscribe(
      res => {
        this.spectacles.splice(this.spectacles.indexOf(entity), 1);
      },
      err => {
        console.log(err);
      })
  };

  pageChange(currentPage: number) {
    this.currentPageNumber = currentPage;
    this.skip = ((currentPage - 1) * this.pageSize);

    this.spectacleService.GetFiltered(this.skip, this.pageSize).subscribe(
      res => {
        this.response = res;
        this.spectacles = this.response.body;
        this.totalRecords = this.response.totalRecords;
      },
      err => {
        console.log(err);
      })

    this.changeTablePage.emit(this.skip);

  }
}
