// import:
import React from "react";
import { useState } from "react"
import SearchResult from "./components/SearchResult";
import SearchForm from "./components/SearchForm";

function App() {

  // set search results:
  const [jobTitle, setJobTitle] = useState()
  const [jobs, setJobs] = useState([])

  // ask for search results from server:
  function search(id) {
    fetch('http://localhost:8080/jobs/' + id)
    .then(response => response.json()) // Extract json from response.
    .then(data => {
        setJobTitle(data.JobTitle.Name)  // Load job title.
        setJobs(data.Jobs)               // load jobs.
    })            
    .catch(e => console.log(e))}; // Log is error is raised.

  return (
    <div className="container">
      <div className="box">
        <h1 className="headline">Job Search</h1>
      </div>
      <SearchForm onClick={search}></SearchForm>
      <SearchResult jobTitle={jobTitle} jobs={jobs}></SearchResult>
    </div>
  );
}
export default App;
