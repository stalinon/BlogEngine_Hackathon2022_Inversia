import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { useParams } from "react-router-dom";
import { Issue } from "../lib/ApiClient";
import { useState, useEffect, forwardRef } from "react";
import { b64toBlob } from "../lib/Helpers";
import HTMLFlipBook from "react-pageflip";
import { pdfjs, Document, Page as ReactPdfPage } from "react-pdf";

pdfjs.GlobalWorkerOptions.workerSrc = `//cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.js`;

const width = 300;
const height = 424;

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

  useEffect(() => {
    !pdf &&
      Issue.getById(id).then((res) => {
        let issuesPdf = res.data.pdf;
        setPdf(URL.createObjectURL(b64toBlob(issuesPdf)));
      });
  }, [pdf, id]);

  return (
    <Theme className="App" preset={presetGpnDefault}>
      <Document file={pdf}>
        <HTMLFlipBook width={width} height={height}>
          <Page pageNumber={1} />
          <Page pageNumber={2} />
          <Page pageNumber={3} />
        </HTMLFlipBook>
      </Document>
    </Theme>
  );
}
