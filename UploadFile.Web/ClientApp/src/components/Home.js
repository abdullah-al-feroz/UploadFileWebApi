import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    state = { file: null, response: null };

    onFileSelect = (e) => {
        console.log(e.target.files);
        this.setState({ file: e.target.files[0] });
    }

    onUpload = (e) => {
        const data = new FormData();

        data.append('files', this.state.file);

        fetch("https://localhost:44353/api/FileUpload", { method: "POST", body: data })
            .then(res => {
                console.log(res);
                return res.json();
            })
            .then(d => this.setState({ file: null, response: d }))
            .catch(e=> console.error(e));
    }

  render () {
    return (
      <div>
            <input type="file" onChange={this.onFileSelect} />
            <button type="button" onClick={this.onUpload}>Upload</button>
            {this.state.response && <p>{JSON.stringify(this.state.response)}</p> }
        </div>
    );
  }
}
