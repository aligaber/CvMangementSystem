import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CvService {

  constructor(private client: HttpClient ) { }

  getCvsList(pageNumber: number, pageSize: number){
    return this.client.get("https://localhost:7098/api/cvManager/GetCvsList", {params: {pageNumber, pageSize}});
  }

  deleteCv(cvId: number){
    return this.client.delete("https://localhost:7098/api/cvManager/delete/"+cvId);
  }

  addCv(Cv: any){
    return this.client.post("https://localhost:7098/api/cvManager/Create", Cv);
  }

  editCv(Cv: any){
    return this.client.put("https://localhost:7098/api/cvManager/Update", Cv);
  }


}
