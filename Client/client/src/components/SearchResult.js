const SearchResult = ({jobTitle, jobs}) => {
  return (
    <>
    {jobs.map((job) => (
      <h3 key={job.ID}>{`${jobTitle} in ${job.City}, ${job.State}`}</h3>
      ))}
    </>
  )
}

export default SearchResult
