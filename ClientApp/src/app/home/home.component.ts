import { Component, OnInit } from '@angular/core';
import { Usuario } from '../Modal';
import { ResponseContentType, RequestOptions } from '@angular/http';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';
import { Observable } from 'rxjs';
import { saveAs } from 'file-saver';
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
  usuarios: any = [];
  ngOnInit() {
    this.BuscaArquivos();
    this.progress = 0
    this.usuario = new Usuario;
  }
  EnviaFoto(files) {
    const formData = new FormData();
    for (let file of files)
      formData.append(file.name, file)
    formData.append('usuario', this.usuario.arquivo);
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
  BuscaArquivos() {
    return this.http.get<Usuario>("/api/Fotos/GetFiles").subscribe(result => {
      this.usuarios = result;
    }, error => console.log(error));
  }
  dowload(foto: string) {
    return this.http.get("/api/Fotos/DowloadFile?arquivo=" + foto, { responseType: 'blob' }).subscribe(blob => {
      saveAs(blob, 'Karsten.png', { type: 'text/plain;charset=windows-1252' });
    });
  }
} 
