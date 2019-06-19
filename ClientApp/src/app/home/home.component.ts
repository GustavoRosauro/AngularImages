import { Component, OnInit } from '@angular/core';
import { Usuario } from '../Modal';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    progress: number;
    message: string;

  constructor(private http:HttpClient) { }
  usuario: any;
  ngOnInit() {
    this.usuario = new Usuario;
  }
  EnviaFoto(files) {
    const formData = new FormData();
    for (let file of files)
      formData.append(file.name, file)
    const uploadReq = new HttpRequest('POST', 'api/Fotos/EnviaFoto', formData, {
      reportProgress: true,
    });
    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type === HttpEventType.Response)
        this.message = event.body.toString();
    });
  }
} 
