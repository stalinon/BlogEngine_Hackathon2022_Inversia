import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { useParams } from "react-router-dom";
import { Issue } from "../lib/ApiClient";
import { useState, useEffect, forwardRef } from "react";
import { b64toBlob } from "../lib/Helpers";
import HTMLFlipBook from "react-pageflip";
import { pdfjs, Document, Page as ReactPdfPage } from "react-pdf";
import { Loader } from "@consta/uikit/Loader";

pdfjs.GlobalWorkerOptions.workerSrc = `//cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.js`;

const width = 2 * 300;
const height = 2 * 424;

const Page = forwardRef(({ pageNumber }, ref) => {
  return (
    <div ref={ref}>
      <ReactPdfPage pageNumber={pageNumber} width={width} />
    </div>
  );
});

export default function IssuePage() {
  const { id } = useParams();
  const [pdf, setPdf] = useState(null);
  const [count, setNumPages] = useState(null);

  const onDocumentLoadSuccess = ({ numPages }) => {
    console.log(numPages);
    setNumPages(numPages);
  };

  useEffect(() => {
    !pdf &&
      Issue.getById(id).then((res) => {
        let issuesPdf = res.data.pdf;
        setPdf(URL.createObjectURL(b64toBlob(issuesPdf)));
      });
  }, [pdf, id]);

  return (
    <Theme className="App" preset={presetGpnDefault} style={{ marginTop: 10 }}>
      {pdf == null ? (
        <Loader />
      ) : (
        <div style={{ marginLeft: "17%" }}>
          <Document file={pdf} onLoadSuccess={onDocumentLoadSuccess}>
            {count && (
              <HTMLFlipBook width={width} height={height}>
                {Array.from(Array(count).keys()).map((x) => {
                  return <Page key={x + 1} pageNumber={x + 1} />;
                })}
              </HTMLFlipBook>
            )}
          </Document>
        </div>
      )}
    </Theme>
  );
}
