import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Tournament } from 'src/app/models/tournament.model';
import { TournamentService } from 'src/app/services/tournament.service';

@Component({
  selector: 'app-view-tournaments',
  templateUrl: './view-tournaments.component.html',
  styleUrls: ['./view-tournaments.component.css']
})
export class ViewTournamentsComponent implements OnInit {

  tournaments$: Observable<Tournament[]>;

  constructor(private tournamentService: TournamentService) { }

  ngOnInit(): void {
    this.tournaments$ = this.tournamentService.getAll();
  }
}
