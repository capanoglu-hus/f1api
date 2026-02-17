/* GET */
export const baseUrl = "https://localhost:7255";

export const drivers = `${baseUrl}/api/driver/Drivers`;
export const races = `${baseUrl}/api/races/Races`;
export const teams = `${baseUrl}/api/team/Teams`;
export const bestTeam = `${baseUrl}/api/Votes/bestTeam`;
export const bestDriver = `${baseUrl}/api/Votes/bestDrivers`;
export const userInfo = `${baseUrl}/api/User/my-profile`;

/* auth */
export const login = `${baseUrl}/api/Auth/login`;
export const register = `${baseUrl}/api/Auth/register`;

/* vote */
export const driverforvote = `${baseUrl}/api/Votes/driverforvote`;
export const teamforvote = `${baseUrl}/api/Votes/teamforvote`;
export const racePrediction = `${baseUrl}/api/Votes/racePrediction`;

/* vote bool */
export const DriverVoteBool = `${baseUrl}/api/Votes/DriverVoteBool`;
export const TeamVoteBool = `${baseUrl}/api/Votes/TeamVoteBool`;
export const RacePredictionBool = `${baseUrl}/api/Votes/RacePredictionBool`;

/* last race ınfo */
export const LastRaceInfo = `${baseUrl}/api/races/Races/GetLastRaceInfo`;

/* */
export const UpdateUserInfo =  `${baseUrl}/api/User/update-my-profile`;