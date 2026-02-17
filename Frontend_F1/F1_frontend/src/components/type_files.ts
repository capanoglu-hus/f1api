// get metotları
export interface DriverTypes{
    id : number;
    name : string;
    racingNumber: number;
    team: string;
    description: string;
    imgUrl:string;
}

export interface TeamTypes{
    id : number;
    name : string;
    principal: string;
    imgUrl: string;
    drivers: DriverTypes[];
}
export interface DriverSummary {
    id?: number;
    name: string;
    racingNumber: number;
    description: string;
}
export interface RaceResult{
    id:number;
    raceName: string;
    raceDate: Date;
    winner: string;
    second:string;
    third:string;
    imgUrl:string;
}

export interface BestTeam {
    teamId: number;
    teamName: string;
    teamPrincipal: string;
    drivers: DriverSummary[]; // API'de 'drivers' yazıyor, 'summarys' değil!
    totalVotes: number;
    voteUpdateDate: string;
}


export interface BestDriver{
    id: number;
    driverName :string;
    racingNumber: number;
    teamName: string;
    description: string;
    totalScore: number;
    totalVotes: number;
    ratedDate: Date;

}

export interface UserInfo{
    id?: number;
    name:string;
    email:string;
    walletAddress?: string;
    createdDate: string;
}

export interface UpdateUser{
    name:string; 
    walletAddress?: string;
}

export interface LoginRequest{
    email:string;
    password:string;
}

export interface RegisterRequest extends LoginRequest {
  name: string;
  passwordConfirm: string; // Sadece frontend kontrolü için
}

export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
}

export interface DriverForVote{
    firstDriverId: number;
    secondDriverId: number;
    thirdDriverId: number;
}

export interface TeamForVote{
    teamId: number;
}
export interface RacePrediction {
    raceId: number;
    firstPlaceDriverId:number;
    secondPlaceDriverId: number;
    thirdPlaceDriverId: number;
}

