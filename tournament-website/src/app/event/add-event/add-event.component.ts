import { Event } from 'src/app/models/event.model';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from 'src/app/services/event.service';
import { TournamentService } from 'src/app/services/tournament.service';
import Swal from 'sweetalert2';
import { Tournament } from 'src/app/models/tournament.model';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.scss']
})
export class AddEventComponent implements OnInit {

  userForm: FormGroup;
  roleList = ['fwfw', 'bdfbfdb'];

  event: Event = {};
  tournaments: Tournament[];
  isUpdate = false;
  today = new Date().toJSON().slice(0, 10);
  minEndDate = '';

  constructor(private eventService: EventService, private tournamentService: TournamentService,
              private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const endDate = new Date();
    endDate.setDate(endDate.getDate() + 1);
    this.minEndDate = endDate.toJSON().slice(0, 10);

    if (this.route.snapshot.params.id){
      this.isUpdate = true;
      this.eventService.get(this.route.snapshot.params.id).subscribe(e => this.event = e);
    }

    this.tournamentService.getAll().subscribe(t => this.tournaments = t);
  }

  saveChanges(form: NgForm): void {
    if (this.isUpdate){
      this.eventService.update(this.route.snapshot.params.id, form.value).subscribe(() => {
        this.handleSuccess();
      });
    }
    else{
      this.tournamentService.create(form.value).subscribe(() => {
        this.handleSuccess();
      });
    }
  }

  cancel(): void {
    this.router.navigate(['../'], { relativeTo: this.route });
  }

  handleSuccess(): void{
    Swal.fire({
      icon: 'success',
      title: 'Event successfully saved.',
      showConfirmButton: false,
      timer: 1500
    });
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
  // initializeForm(): void {
  //   this.userForm = this.fb.group({
  //     userId: [''],
  //     firstName: ['', [Validators.required]],
  //     lastName: ['', [Validators.required]],
  //     email: ['', [Validators.required, Validators.email]],
  //     phone: [
  //       '',
  //       [Validators.required, Validators.maxLength(12), Validators.minLength(10)],
  //     ],
  //     postalCode: ['', [Validators.required, Validators.maxLength(8), Validators.minLength(4)]],
  //     companyName: [''],
  //     companyNumber: [''],
  //     phoneNumberAlternate: ['', [Validators.required, Validators.maxLength(12), Validators.minLength(10)]],
  //     dateOfBirth: [null, Validators.compose([Validators.required])],
  //     lockoutEnabled: [false],
  //     roleName: ['', [Validators.required]]
  //   });
  // }
