import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})

export class SpectacleService {
  constructor(public httpClient: HttpClient) {
  }

  public readonly BaseUrl = "http://localhost:54277/api";
  GetAll()
  {
    return this.httpClient.get(this.BaseUrl + "/spectacles");
  }

  GetFiltered(skip: number, take: number) {
    return this.httpClient.get(this.BaseUrl + "/spectacles/getfiltered/" + skip + "/" + take);
  }

  GetById(id: string) {
    return this.httpClient.get(this.BaseUrl + "/spectacles/getbyid/" + id);
  }

  Delete(entity)
  {
    return this.httpClient.delete(this.BaseUrl + "/spectacles/" + entity.id);
  }

  AddSession(spectacle, session)
  {
    session.StartDateTime = new Date(session.StartDateTime.year, session.StartDateTime.month, session.StartDateTime.day, 0, 0, 0, 0);
    return this.httpClient.post(this.BaseUrl + "/spectacles/" + spectacle.id + "/addsession", session);
  }

  Reserve(session) {
    return this.httpClient.post(this.BaseUrl + "/spectacles/" + session.id + "/reserve", null);
  }
}
