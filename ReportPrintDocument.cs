using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Microsoft.Reporting.WinForms;

public class ReportPrintDocument : PrintDocument
{
    private readonly LocalReport _localReport;
    private PageSettings _pageSettings;
    private int _currentPageIndex;
    private IList<Stream> _pages;

    public ReportPrintDocument(LocalReport localReport)
    {
        _localReport = localReport ?? throw new ArgumentNullException(nameof(localReport));

        // Initialize default page settings
        _pageSettings = new PageSettings
        {
            PaperSize = new PaperSize("Custom", 850, 1100), // 8.5 x 11 inches in hundredths of an inch
            Margins = new Margins(10, 10, 10, 10) // Set margins (left, right, top, bottom)
        };
    }

    public PageSettings DefaultPageSettings
    {
        get => _pageSettings;
        set
        {
            _pageSettings = value;
            // Manually set page size for the report without SetPageSettings
        }
    }

    protected override void OnBeginPrint(PrintEventArgs e)
    {
        base.OnBeginPrint(e);

        _currentPageIndex = 0;
        _pages = new List<Stream>();

        // Render the report as images (one stream per page)
        _localReport.Render("Image", CreateDeviceInfo(), CreateStream, out _);
    }

    protected override void OnPrintPage(PrintPageEventArgs e)
    {
        base.OnPrintPage(e);

        if (_currentPageIndex >= _pages.Count)
        {
            e.HasMorePages = false;
            return;
        }

        using (var pageImage = Image.FromStream(_pages[_currentPageIndex]))
        {
            Rectangle printArea = e.PageBounds;
            e.Graphics.DrawImage(pageImage, printArea);
        }

        _currentPageIndex++;
        e.HasMorePages = _currentPageIndex < _pages.Count;
    }

    protected override void OnEndPrint(PrintEventArgs e)
    {
        base.OnEndPrint(e);

        if (_pages != null)
        {
            foreach (var stream in _pages)
                stream.Dispose();
            _pages = null;
        }
    }

    private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
    {
        var stream = new MemoryStream();
        _pages.Add(stream);
        return stream;
    }

    private string CreateDeviceInfo()
    {
        // Set device info for page settings directly here
        return $@"
            <DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>{_pageSettings.PaperSize.Width / 100.0}in</PageWidth>
                <PageHeight>{_pageSettings.PaperSize.Height / 100.0}in</PageHeight>
                <MarginTop>{_pageSettings.Margins.Top / 100.0}in</MarginTop>
                <MarginLeft>{_pageSettings.Margins.Left / 100.0}in</MarginLeft>
                <MarginRight>{_pageSettings.Margins.Right / 100.0}in</MarginRight>
                <MarginBottom>{_pageSettings.Margins.Bottom / 100.0}in</MarginBottom>
            </DeviceInfo>";
    }
}

