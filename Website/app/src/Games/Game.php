<?php

namespace App\Games;

use App\Games\HighScore;
use SilverStripe\Assets\Image;
use SilverStripe\ORM\DataObject;
use SilverStripe\Security\Permission;

/**
 * Class \App\Database\Experience
 *
 * @property string $Title
 * @property string $Description
 * @property int $BackgroundImageID
 * @method \SilverStripe\Assets\Image BackgroundImage()
 * @method \SilverStripe\ORM\DataList|\App\Games\HighScore[] Highscores()
 */
class Game extends DataObject
{
    private static $db = [
        "Title" => "Varchar(255)",
        "Description" => "Text",
    ];

    private static $has_one = [
        "BackgroundImage" => Image::class
    ];

    private static $has_many = [
        "Highscores" => HighScore::class,
    ];

    private static $summary_fields = [
    ];

    private static $field_labels = [
    ];

    private static $default_sort = "ID ASC";

    private static $table_name = "Game";

    private static $singular_name = "Game";
    private static $plural_name = "Games";

    private static $url_segment = "games";

    public function onBeforeWrite()
    {
        parent::onBeforeWrite();
    }

    public function getCMSFields()
    {
        $fields = parent::getCMSFields();
        return $fields;
    }

    public function canView($member = null)
    {
        return true;
    }

    public function canEdit($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canDelete($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canCreate($member = null, $context = [])
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }
}
