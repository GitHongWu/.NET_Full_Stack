import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from 'src/app/core/services/movie.service';
import { MovieDetail } from 'src/app/shared/models/moviedetail';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {

  constructor(private route: ActivatedRoute, private movieService: MovieService) { }

  id!: number;
  movie!: MovieDetail;

  ngOnInit(): void {
    // read the id from the Route
    console.log('inside Movie details page');
    this.route.paramMap.subscribe(
      params => {
        console.log(params);
        console.log(params.get('id'));
        this.id = Number(params.get('id'));
        console.log('Movie Id:' + this.id);

        // call the MovieService that will call the Movie Details API.
        this.movieService.getMovieById(this.id).subscribe(
          m => {
            this.movie = m;
            console.table(this.movie);
            // console.log(this.movie.values);
          }
        );

      }
    )
  }

}