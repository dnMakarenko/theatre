import { SpectacleService } from '.././../spectacle/spectacle.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../shared/user.service';

@Component({
  selector: 'app-spectacle-details',
  templateUrl: './spectacle.details.component.html'
})
export class SpectacleDetailsComponent implements OnInit {
  spectacle: any;
  sessionFormModel = {
    SpectacleId: "",
    StartDateTime: new Date(),
    DurationInMinutes: "",
    MaxNumberOfTickets: ""
  };

  constructor(private spectacleService: SpectacleService,
    private route: ActivatedRoute,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private userService: UserService)
  { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      console.log(params.get('id'))
      this.spectacleService.GetById(params.get('id')).subscribe(obj => {
        this.spectacle = obj;
        console.log(obj);
      })
    });
  }

  OpenSessionModal(modal, spectacle) {
    this.modalService.open(modal, {
      size: 'lg', backdrop: 'static', keyboard: false
    })
  }

  CloseSessionModal() {
    this.modalService.dismissAll('Closed');
  }

  PostSessionModal()
  {
    this.spectacleService.AddSession(this.spectacle, this.sessionFormModel).subscribe(
      res => {
        this.toastr.success('New session created!', 'Session added to spectacle.');
        this.spectacle = res;
      },
      err => {
        this.toastr.error('Could not create session!', 'Session not added to spectacle.');
      });
  }

  Reserve(session) {
    this.spectacleService.Reserve(session).subscribe(
      res =>
      {
        this.toastr.success('New reserved created!', 'Reservation added to spectacle session.');
        session.reservations.push(res);
      },
      err =>
      {
        this.toastr.error('Could not reserve session!', 'Session not reserved.');
      });
  }
}
