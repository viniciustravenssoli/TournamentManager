export interface Tournament {
  tournamentId: number;
  TournamentName: string;
  participants: Participant[];
  matches: Match[];
}

export interface TournamentToList {
  tournamentId: number;
  name: string;
  participants: Participant[];
  matches: Match[];
}

export interface Participant {
  participantId: number;
  name: string;
}

export interface Match {
  matchId: number;
  tournamentId: number;
  participant1Id: number;
  participant1Name: string;
  participant2Id: number;
  participant2Name: string;
  winnerId: number | null;
  winnerName: string | null;
  round: number;
}

export interface AuthResponse {
  name: string;
  token: string;
}
