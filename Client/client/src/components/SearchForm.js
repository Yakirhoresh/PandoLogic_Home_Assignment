import { useState } from "react"

const SearchForm = ({onClick}) => {

  // set search autocomplete suggestions:
  const [jobTitles, setJobTitles] = useState([])

  // ask for autocomplete suggestions from server:
  function autocomplete(searchValue) {
    fetch('http://localhost:8080/jobTitles/' + searchValue)
    .then(response => response.json()) // Extract json from response.
    .then(data => {
        setJobTitles(data)  // Load job titles.
    })            
    .catch(e => console.log(e))}; // Log is error is raised.


  return (
    <div className="add-form">
        <div className="form-control">
            <input type='text' onChange={(e) => autocomplete(e.target.value)} placeholder="Enter Job Title"></input>
        </div>
        <div>
            {jobTitles.map((title) => (
            <button key={title.ID} onClick={() => onClick(title.ID)} className='btn-white btn-block'>{`${title.Name}`}</button>
            ))}
        </div>
    </div>
  )
}

export default SearchForm
