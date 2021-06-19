export interface Genre {
    id: number;
    name: string;
}

export interface Cast {
    id: number;
    name: string;
    gender: string;
    tmdbUrl: string;
    profilePath: string;
    character: string;
}

export interface MovieDetail{
    id: number;
    title: string;
    posterUrl: string;
    backdropUrl: string;
    rating: number;
    overview: string;
    tagline: string;
    budget: number;
    revenue: number;
    imdbUrl: string;
    tmdbUrl: string;
    runTime: number;
    price: number;
    releaseDate: Date;
    genres: Genre[];
    casts: Cast[];
}